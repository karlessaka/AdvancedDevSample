# Architecture Technique

Le projet **AdvancedDevSample** suit les principes de la **Clean Architecture** (aussi appelée Architecture Hexagonale ou en Oignon).

Cette structure permet de :
* Isoler la logique métier (Domain) des dépendances externes (BDD, API).
* Faciliter les tests unitaires.
* Rendre le code maintenable et évolutif.

## 1. Diagramme de Classes (Structure)

Ce schéma montre comment les couches interagissent. Notez que le **Service** dépend d'une **Interface**, et non de la base de données directement (Inversion de Dépendance).

```mermaid
classDiagram
    %% --- DOMAIN ---
    class Product {
        +Guid Id
        +int StockQuantity
        +UpdateStock()
    }

    class Order {
        +Guid Id
        +Guid ClientId
        +AddItem()
    }

    %% --- SERVICES ---
    class ProductService {
        +CreateProduct()
    }

    class OrderService {
        +PlaceOrder()
    }

    %% --- INTERFACES ---
    class IProductRepositoryAsync { <<Interface>> }
    class IOrderRepositoryAsync { <<Interface>> }

    %% RELATIONS
    OrderService --> IProductRepositoryAsync : 1. Vérifie & Update Stock
    OrderService --> IOrderRepositoryAsync : 2. Sauvegarde Commande
    
    OrderService ..> Product : Manipule
    OrderService ..> Order : Crée
    
    Order "1" *-- "*" OrderItem : Contient

## 3. Focus : Le Processus de Commande (Cross-Domain)

La gestion des commandes est une opération complexe qui implique deux domaines : **Order** et **Product**.
Le `OrderService` joue le rôle d'orchestrateur pour garantir l'intégrité des données (Stock).

### Diagramme de Séquence : Passer une commande

Ce diagramme détaille les étapes de validation du stock avant la création de la commande.

```mermaid
sequenceDiagram
    participant Client as Client
    participant API as OrdersController
    participant Service as OrderService
    participant ProductRepo as ProductRepository
    participant DomainProd as Product (Entity)
    participant DomainOrder as Order (Entity)
    participant OrderRepo as OrderRepository

    Client->>API: POST /api/orders (ClientId, ProductId, Qty)
    API->>Service: PlaceOrderAsync(request)
    
    rect rgb(230, 240, 255)
        note right of Service: 1. Vérification du Produit
        Service->>ProductRepo: GetByIdAsync(productId)
        ProductRepo-->>Service: Product
        
        Service->>DomainProd: Vérifie Stock > Qty ?
        
        alt Stock Insuffisant
            Service-->>API: Erreur "Stock Insuffisant"
            API-->>Client: 400 Bad Request
        else Stock OK
            note right of Service: 2. Mise à jour du Stock
            Service->>DomainProd: UpdateStock(-Qty)
            
            note right of Service: 3. Création Commande
            Service->>DomainOrder: new Order(...)
            Service->>DomainOrder: AddItem(product, qty)
            
            note right of Service: 4. Sauvegarde Globale
            Service->>ProductRepo: UpdateAsync(product)
            Service->>OrderRepo: AddAsync(order)
            
            Service-->>API: Retourne OrderId
            API-->>Client: 200 OK
        end
    end
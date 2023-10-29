# Widget & Co E-commerce System 🛒

## Introduction
This project is designed to handle the robust needs of the Widget & Co e-commerce platform, which experiences heavy traffic, especially during peak hours. We leverage Azure CosmosDB, a globally distributed, multi-model database service, to ensure high availability and low-latency access to our data.

## Why CosmosDB? 🌍
CosmosDB is selected for this project due to its ability to scale horizontally, providing the capability to handle massive amounts of traffic and data seamlessly. With its global distribution, it ensures that our services remain highly available, even during sudden traffic surges.

## How We Handle Data 📊
We're using a reference-based approach in CosmosDB. Given the relationships between our entities range from one-to-many to many-to-many, it is a more efficient approach.

## How We Handle Orders 📦
To address challenges with order placements:

- **Queue Trigger Function:** When an order is initiated, it's placed into a queue. This function then processes the queue to check the availability of products in stock. If stock levels are sufficient, the order is confirmed; otherwise, the order fails, ensuring stock consistency.
  
- **HTTP PUT Trigger Function:** This function is designed to update the status of an order, adding the shipping date. This approach mirrors real-world CMS operations, such as when a delivery person marks an order as shipped after pressing a button.

## Reviews 💬
### User Reviews
Customers can post product reviews, which can serve as valuable feedback for both other customers and our internal teams. Furthermore, our marketing department can leverage these reviews for insights and strategy formulation.

## Sharing Data, the Smart Way 🔄
We've got some partner departments (like good ol' Willy & Co.) that sometimes need user data. So, we use the user data in its own container. They get what they need, and nothing extra.

## Product Images 📸
Add a product, and its pic goes straight to Azure Blob storage with a unique name. We then save all the details in CosmosDB.

---

**N.B:** Not all CRUD functionalities are implemented; only those specifically requested for this assignment. Additionally, Blob storage has not been implemented as ideally intended.

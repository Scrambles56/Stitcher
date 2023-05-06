# Stitcher

This project is here to learn and showcase HotChocolate schema stitching capabilities. 
It operates entirely in-memory as persistence mechanisms are already a well-known problem.

This example project is a conceptual cafe. It has a menu (products), a list of customers and a list of sales.
Each of these sections is a separate GraphQL schema (and application). 
The schemas are stitched together into a single schema.

## Running the project
Run all the downstream services first, and then followed by the stitcher (Gateway). 

---

### Experiment results: 

#### Type Stitching:
We able to achieve type stitching by using the `@delegate` directive. 
This needs to be defined in the `Stitching.graphql` file in the Sticher App. 

In this example, there is type stitching available on the `Transactions` type. 
We are stitching both the customer, and product types onto the transaction type.

To see this, execute the following queries against the Stitcher App:

```graphql
mutation AddCustomer {
    addCustomer(firstName: "John", lastName: "Smith") {
        customerId
    }
}

mutation PurchaseItem {
    purchaseItem(customerId: 2, productId: 2, amount: 5.0) {
        id
        customerId
        transactionType
    }
}

query GetCustomerTransactions {
    transactionsForCustomer(customerId: 2) {
        id,
        transactionType,
        transactionDate,
        amount,
        product {
            productId
            name
        }
        customer {
            customerId
            firstName
            lastName
        }
    }
}
```
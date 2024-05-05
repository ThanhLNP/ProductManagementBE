## Accounts

The application supports different levels of access, with specific credentials for the admin and user accounts as follows:

### Admin Account

- **Email:** admin@example.com
- **Password:** adminP@ssw0rd

The admin account has full access to the application, allowing for the management of products, categories

### User Account

- **Email:** user@example.com
- **Password:** userP@ssw0rd

The user account is intended for general application use. This account does not have administrative privileges.

## Data Demo

To demonstrate adding a product to the application, use the following JSON object as a template for the product creation request:

```json
{
  "name": "Macbook",
  "price": 10,
  "brand": "string",
  "description": "string",
  "categoryId": "d5ded2d9-ff49-4edb-ae1f-7eb650b2c2da",
  "attributes": "{\"ram_gb\": 128}"
}

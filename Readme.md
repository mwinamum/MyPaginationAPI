# MyPaginationAPI

A simple and efficient pagination API to help you manage large datasets with ease.

## Features

- RESTful endpoints for paginated data retrieval
- Easy integration with any frontend or backend
- OpenAPI/Swagger documentation

## Getting Started

The API is publicly hosted and ready to use.

**Base URL:**  
`https://mypaginationapi.onrender.com`

## Documentation

Interactive API docs are available at:  
[https://mypaginationapi.onrender.com/swagger/index.html](https://mypaginationapi.onrender.com/swagger/index.html)

## Example Usage

```http
GET /api/items?page=1&pageSize=10
```

**Response:**
```json
{
    "data": [...],
    "page": 1,
    "pageSize": 10,
    "totalPages": 5,
    "totalItems": 50
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
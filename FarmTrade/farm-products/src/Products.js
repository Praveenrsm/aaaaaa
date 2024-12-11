import React, { useState, useEffect } from 'react';

const ProductsPage = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        fetch('http://localhost:5274/api/Products/GetAllProducts')
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Failed to fetch products');
                }
                return response.json();
            })
            .then((data) => {
                console.log('Fetched products:', data); // Logs fetched data
                setProducts(data);
                setLoading(false);
            })
            .catch((error) => {
                console.error('Error:', error);
                setError(error.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div>
            <h1>Products</h1>
            <div className="products-list">
                {products.length > 0 ? (
                    products.map((product) => (
                        <div key={product.productId} className="product-card">
                            <h2>{product.productName}</h2>
                            <p>{product.description}</p>
                            <p>Price: ${product.productPrice.toFixed(2)}</p>
                            {product.productImages && product.productImages.length > 0 ? (
                                <img
                                    src={`data:image/png;base64,${product.productImages[0].imageData}`}
                                    alt={product.productName}
                                    width="200"
                                />
                            ) : (
                                <p>No image available</p>
                            )}
                        </div>
                    ))
                ) : (
                    <p>No products available</p>
                )}
            </div>
        </div>
    );
};

export default ProductsPage;

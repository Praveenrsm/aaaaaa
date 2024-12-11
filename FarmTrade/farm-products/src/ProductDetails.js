// ProductDetails.js
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const ProductDetails = () => {
    const { productId } = useParams();  // Capture productId from URL parameters
    const [product, setProduct] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchProductDetails = async () => {
            try {
                const response = await axios.get(`http://localhost:5274/api/Products/${productId}`);
                setProduct(response.data); // Set the product data from API response
                setLoading(false);
            } catch (error) {
                console.error('Error fetching product details:', error);
                setLoading(false);
            }
        };

        fetchProductDetails();
    }, [productId]);

    if (loading) return <p>Loading product details...</p>;
    if (!product) return <p>Product not found.</p>;

    return (
        <div>
            <h2>{product.productName}</h2>
            {product.productImage ? (
                <img src={`data:image/png;base64,${product.productImage}`} alt={product.productName} />
            ) : (
                <p>No image available</p>
            )}
            <p>Description: {product.description}</p>
            <p>Price: ${product.productPrice}</p>
            <p>Available Quantity: {product.availableQuantity}</p>
            <p>Seller: {product.user?.userName}</p>
            <h3>Reviews:</h3>
            {product.reviews && product.reviews.length > 0 ? (
                <ul>
                    {product.reviews.map((review) => (
                        <li key={review.id}>
                            <p>Comment: {review.comments}</p>
                            <p>Rating: {review.rating}</p>
                            <p>Reviewed by: {review.user?.userName}</p>
                        </li>
                    ))}
                </ul>
            ) : (
                <p>No reviews available for this product.</p>
            )}
        </div>
    );
};

export default ProductDetails;

// admin.js
import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Admin = () => {
    const [products, setProducts] = useState([]);
    const [selectedProduct, setSelectedProduct] = useState(null);
    const [productImage, setProductImage] = useState(null);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const response = await axios.get("http://localhost:5274/api/Products/GetAllProducts");
                setProducts(response.data);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        fetchProducts();
    }, []);

    const handleProductClick = (product) => {
        setSelectedProduct(product);  // Set the selected product to display details
    };

    const handleImageUpload = (event) => {
        setProductImage(event.target.files[0]);  // Store the selected file in state
    };

    const handleImageSubmit = async () => {
        if (!productImage || !selectedProduct) return;

        // Prepare form data for image upload
        const formData = new FormData();
        formData.append('productImage', productImage);

        try {
            await axios.put(
                `http://localhost:5274/api/Products/${selectedProduct.productId}/UploadImage`,
                formData,
                { headers: { 'Content-Type': 'multipart/form-data' } }
            );
            alert('Image uploaded successfully!');
        } catch (error) {
            console.error('Error uploading image:', error);
        }
    };

    return (
        <div>
            <h1>Admin - Product List</h1>
            <ul>
                {products.map((product) => (
                    <li key={product.productId} onClick={() => handleProductClick(product)}>
                        {product.productName}
                    </li>
                ))}
            </ul>

            {selectedProduct && (
                <div>
                    <h2>Product Details</h2>
                    <p><strong>Name:</strong> {selectedProduct.productName}</p>
                    <p><strong>Price:</strong> ${selectedProduct.productPrice}</p>
                    <p><strong>Description:</strong> {selectedProduct.description}</p>
                    <p><strong>Available Quantity:</strong> {selectedProduct.availableQuantity}</p>

                    {selectedProduct.productImage ? (
                        <img
                            src={`data:image/png;base64,${selectedProduct.productImage}`}
                            alt={selectedProduct.productName}
                            style={{ width: '100px', height: '100px' }}
                        />
                    ) : (
                        <p>No image available</p>
                    )}

                    <h3>Upload New Image</h3>
                    <input type="file" onChange={handleImageUpload} />
                    <button onClick={handleImageSubmit}>Upload Image</button>
                </div>
            )}
        </div>
    );
};

export default Admin;

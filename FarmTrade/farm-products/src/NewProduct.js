import React, { useState } from 'react';
import axios from 'axios';

function AddProduct() {
    const [productName, setProductName] = useState('');
    const [productPrice, setProductPrice] = useState(0);
    const [description, setDescription] = useState('');
    const [availableQuantity, setAvailableQuantity] = useState(0);
    const [images, setImages] = useState([]);

    const handleImageUpload = (event) => {
        const files = event.target.files;
        const imageArray = Array.from(files);

        // Convert each image to base64 string
        const promises = imageArray.map((file) => {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => resolve(reader.result.split(',')[1]); // Remove "data:image/png;base64,"
                reader.onerror = (error) => reject(error);
            });
        });

        Promise.all(promises)
            .then(base64Images => setImages(base64Images))
            .catch(error => console.error("Error uploading images:", error));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        // Ensure proper data types
        const productData = {
            product: { // Ensure correct case
                productName,
                productPrice, // Convert to number
                description,
                availableQuantity, // Convert to number
                userId: "346d952d-a87f-4735-a4bc-a63b300e6491" // Ensure user ID is provided here
            },
            imageList: images // Ensure correct case
        };

        try {
            const response = await axios.post('http://localhost:5274/api/Products/AddProduct', productData);
            console.log(response.data);
            alert("Product added successfully!");
        } catch (error) {
            console.error("Error adding product:", error);
            alert("Failed to add product: " + (error.response?.data?.title || error.message)); // Enhanced error message
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Product Name:</label>
                <input type="text" value={productName} onChange={(e) => setProductName(e.target.value)} required />
            </div>
            <div>
                <label>Product Price:</label>
                <input type="number" value={productPrice} onChange={(e) => setProductPrice(e.target.value)} required />
            </div>
            <div>
                <label>Description:</label>
                <textarea value={description} onChange={(e) => setDescription(e.target.value)} required />
            </div>
            <div>
                <label>Available Quantity:</label>
                <input type="number" value={availableQuantity} onChange={(e) => setAvailableQuantity(e.target.value)} required />
            </div>
            <div>
                <label>Upload Images:</label>
                <input type="file" multiple onChange={handleImageUpload} accept="image/*" />
            </div>
            <button type="submit">Add Product</button>
        </form>
    );
}

export default AddProduct;

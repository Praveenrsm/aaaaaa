import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Admin from './admin';
import ProductDetails from './ProductDetails';
import Login from './Login';
import ProductsPage from './Products';
import AddProduct from './NewProduct';
import ForgotPassword from './ForgotPassword';
import ResetPassword from './ResetPassword';
const App = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/admin" element={<Admin />} />
                <Route path="/admin/product/:productId" element={<ProductDetails />} />
                <Route path="/NewProduct" element={<AddProduct />} />
                <Route path="/Products" element={<ProductsPage />} />
                <Route path="/ForgotPassword" element={<ForgotPassword />} />
                <Route path="/ResetPassword" element={<ResetPassword />} />
            </Routes>
        </Router>
    );
};

export default App;

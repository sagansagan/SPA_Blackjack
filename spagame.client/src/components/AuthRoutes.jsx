import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const AuthRoutes = ({ isAuthenticated }) => {
    return isAuthenticated ? <Outlet /> : <Navigate to="/login" />;
};

export default AuthRoutes;
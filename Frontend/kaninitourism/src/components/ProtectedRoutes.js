import React from 'react';
import { Navigate } from 'react-router-dom';

const ProtectedRoutes = ({ element, allowedRoles }) => {
  const user = JSON.parse(localStorage.getItem('Credentials')); // You need to adjust this based on your data structure

  if (!user || !allowedRoles.includes(user.role)) {
    // Redirect to an unauthorized page or login page
    return <Navigate to="/unauthorized" />;
  }

  return element;
};

export default ProtectedRoutes;

import React, { useContext } from 'react';
import AuthContext from '../context/authcontext';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
    const { setIsAuth } = useContext(AuthContext);
    const navigate = useNavigate();
    
    const handleLogout = () => {
        localStorage.removeItem('token');
        setIsAuth(false);
        navigate('/', { replace: true }); // Заменяем текущую запись в истории
    };

    handleLogout();

    return null;
};

export default Logout;
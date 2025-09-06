import React, { useState, useEffect } from 'react';
import AuthContext from '../context/authcontext'

const AuthProvider = ({ children }) => {
    const [isAuth, setIsAuth] = useState(false);

    useEffect(() => {
      const token = localStorage.getItem('token');
      if (token) {
        setIsAuth(true);
      }
    }, []);

    return (
      <AuthContext.Provider value={{ isAuth, setIsAuth }}>
        {children}
      </AuthContext.Provider>
    );
};

export default AuthProvider;

  
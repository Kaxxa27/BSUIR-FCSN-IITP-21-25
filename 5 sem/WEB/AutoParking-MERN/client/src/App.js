import React from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import Navbar from './components/UI/Navbar/Navbar';
import AppRouter from './components/AppRouter';
import AuthProvider from './components/AuthProvider';

import "./styles/App.css"

const App = () => {
    return (
        <Router>
            <AuthProvider>
                <div className='App'>
                    <Navbar />
                    <AppRouter />
                </div>
            </AuthProvider>
        </Router>
    );
};

export default App;


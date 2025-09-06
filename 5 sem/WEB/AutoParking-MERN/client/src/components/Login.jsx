import React, { useContext, useState } from 'react';
import MainButton from './UI/button/MainButton';
import $api from '../http/index';
import AuthContext from '../context/authcontext';
import { useNavigate } from 'react-router-dom';

const Login = () => {
   const {setIsAuth} = useContext(AuthContext);
   const [email, setEmail] = useState('');
   const [password, setPassword] = useState('');
   const navigate = useNavigate();

   const handleLogin = async (e) => {
      e.preventDefault();
      try {
         const response = await $api.post('/auth/login', { email, password });
         const { token } = response.data;

         localStorage.setItem('token', token);
         setIsAuth(true);
         navigate('/');
      } catch (error) {
         alert(`Введите корректные данные!\n${error.response.data.message}`)
         console.error('Error during login:', error);
      }
   };

   return (
      <div>
         <h2>Login</h2>
         <form>
            <label>Email:</label>
            <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <br />
            <label>Password:</label>
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <br />
            <button onClick={handleLogin}>Login</button>
         </form>
      </div>
   );
};

export default Login;

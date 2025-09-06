import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AuthContext from '../context/authcontext';
import $api from '../http/index';


const Registration = () => {
  const { setIsAuth} = useContext(AuthContext);
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    username: '',
    name: '',
    age: 0,
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {

      await $api.post('/auth/register', formData);

      setIsAuth(true);

      navigate('/');
    } catch (error) {
      alert(`Введите корректные данные при регистрации!\n ${error.response.data.message}`);
      console.error('Error in registration!', error.message);
    }
  };

  return (
    <div>
      <h2>Регистрация</h2>
      <form onSubmit={handleSubmit}>
        <label>Email:</label>
        <input type="email" name="email" value={formData.email} onChange={handleChange} required />

        <label>Password:</label>
        <input type="password" name="password" value={formData.password} onChange={handleChange} required />

        <label>Username:</label>
        <input type="text" name="username" value={formData.username} onChange={handleChange} required />

        <label>Name:</label>
        <input type="text" name="name" value={formData.name} onChange={handleChange} required />

        <label>Age:</label>
        <input type="number" name="age" value={formData.age} onChange={handleChange} required />

        <button type="submit">Зарегистрироваться</button>
        <button type="button" onClick={() => navigate('/login')}>Войти</button>
      </form>
    </div>
  );
};

export default Registration;

import React, { useState } from 'react';
import MyModal from '../../MyModal/MyModal';
import $api from '../../../../http/index'
import { useNavigate } from 'react-router-dom';

const CreateUserForm = ({ visible, setVisible, loadUsers }) => {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        password: '',
        age: 0
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await $api.post('/users', formData);
            loadUsers();
            navigate('/users-catalog');
        } catch (error) {
            alert(`Введите корректные данные!\n ${error.response.data.message}`);
            console.error('Error in create user!', error.message);
        }
        setVisible(false);
    };

    return (
        <MyModal visible={visible} setVisible={setVisible}>
            <h2>Создать пользователя</h2>
            <form onSubmit={handleSubmit}>

                <label>Username:</label>    
                <input type="text" name="username" value={formData.username} onChange={handleChange}/>

                <label>Email:</label>
                <input type="email" name="email" value={formData.email} onChange={handleChange} required />

                <label>Password:</label>
                <input type="password" name="password" value={formData.password} onChange={handleChange} required />
                
                <label>Age:</label>
                <input type="number" name="age" value={formData.age} onChange={handleChange} />

                <button type="submit">Создать</button>
                <button onClick={() => setVisible(false)}>Отмена</button>
            </form>
          
        </MyModal>
    );
};

export default CreateUserForm;
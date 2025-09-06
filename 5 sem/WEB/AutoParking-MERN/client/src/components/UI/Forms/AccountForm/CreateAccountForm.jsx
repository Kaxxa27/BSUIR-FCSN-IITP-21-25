import React, { useState } from 'react';
import MyModal from '../../MyModal/MyModal';
import $api from '../../../../http/index'
import { useNavigate } from 'react-router-dom';

const CreateAccountForm = ({ visible, setVisible, loadAccounts }) => {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        amount: 0
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
            await $api.post('/accounts', formData);
            loadAccounts();
            navigate('/accounts-catalog');
        } catch (error) {
            alert(`Введите корректные данные!\n ${error.response.data.message}`);
            console.error('Error in create Account!', error.message);
        }
        setVisible(false);
    };

    return (
        <MyModal visible={visible} setVisible={setVisible}>
            <h2>Создать счет</h2>
            <form onSubmit={handleSubmit}>

                <label>Amount:</label>    
                <input type="number" name="amount" value={formData.amount} onChange={handleChange}/>
                
                <button type="submit">Создать</button>
                <button onClick={() => setVisible(false)}>Отмена</button>
            </form>
          
        </MyModal>
    );
};

export default CreateAccountForm;
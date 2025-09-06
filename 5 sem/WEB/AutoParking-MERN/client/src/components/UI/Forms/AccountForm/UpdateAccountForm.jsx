import React, { useState, useEffect } from 'react';
import MyModal from '../../MyModal/MyModal';
import $api from '../../../../http/index'
import { useNavigate } from 'react-router-dom';

const UpdateAccountForm = ({ Account, visible, setVisible, loadAccounts }) => {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        amount: 0
    });

    useEffect(() => {
        if (Account) {
            setFormData({
              ...Account
            });
        }
    }, []);

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

            await $api.put(`/accounts/${Account._id}`, formData);
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
            <h2>Обновить счет</h2>
            <form onSubmit={handleSubmit}>

                <label>Amount:</label>    
                <input type="number" name="amount" value={formData.amount} onChange={handleChange}/>

                <button type="submit">Обновить</button>
                <button onClick={() => setVisible(false)}>Отмена</button>
            </form>

        </MyModal>
    );
};

export default UpdateAccountForm;
import React, { useState } from 'react';
import MyModal from '../../MyModal/MyModal';
import $api from '../../../../http/index'
import { useNavigate } from 'react-router-dom';

const CreateParkingForm = ({ visible, setVisible, loadParkingSpots }) => {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        number: 0,
        price: 0
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
            await $api.post('/parkingspots', formData);
            loadParkingSpots();
            navigate('/parking-catalog');
        } catch (error) {
            alert(`Введите корректные данные!\n ${error.response.data.message}`);
            console.error('Error in create parking!', error.message);
        }
        setVisible(false);
    };

    return (
        <MyModal visible={visible} setVisible={setVisible}>
            <h2>Создать парковку</h2>
            <form onSubmit={handleSubmit}>

                <label>Номер:</label>    
                <input type="number" name="number" value={formData.number} onChange={handleChange} />

                <label>Цена:</label>
                <input type="number" name="price" value={formData.price} onChange={handleChange} required />

                <button type="submit">Создать</button>
                <button onClick={() => setVisible(false)}>Отмена</button>
            </form>
          
        </MyModal>
    );
};

export default CreateParkingForm;
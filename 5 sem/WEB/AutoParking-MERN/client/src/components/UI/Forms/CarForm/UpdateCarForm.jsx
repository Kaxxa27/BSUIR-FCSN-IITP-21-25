import React, { useState, useEffect } from 'react';
import MyModal from '../../MyModal/MyModal';
import $api from '../../../../http/index'
import { useNavigate } from 'react-router-dom';

const UpdateCarForm = ({ Car, visible, setVisible, loadCars }) => {

    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        model: '',
        mark: '',
        license_plate: '',
    });

    useEffect(() => {
        if (Car) {
            setFormData({
              ...Car
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

            await $api.put(`/Cars/${Car._id}`, formData);
            loadCars();
            navigate('/Cars-catalog');
        } catch (error) {
            alert(`Введите корректные данные!\n ${error.response.data.message}`);
            console.error('Error in create Car!', error.message);
        }
        setVisible(false);
    };

    return (
        <MyModal visible={visible} setVisible={setVisible}>
            <h2>Обновить машину</h2>
            <form onSubmit={handleSubmit}>

                <label>Model:</label>    
                <input type="text" name="model" value={formData.model} onChange={handleChange}/>

                <label>Mark:</label>
                <input type="text" name="mark" value={formData.mark} onChange={handleChange} />

                <label>License Plate:</label>
                <input type="text" name="license_plate" value={formData.license_plate} onChange={handleChange} required />

                <button type="submit">Обновить</button>
                <button onClick={() => setVisible(false)}>Отмена</button>
            </form>

        </MyModal>
    );
};

export default UpdateCarForm;
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import MyModal from '../MyModal/MyModal';
import $api from '../../../http/index';

const GeneralForm = ({form, visible, setVisible}) => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({});

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
      await $api.post(`/${form.modelURL}`, formData);
      navigate('/'); // Измените на соответствующий путь
    } catch (error) {
      alert(`Введите корректные данные!\n ${error.response.data.message}`);
      console.error(`Error`, error.response.data.message);
    }
    setVisible(false);
  };

  return (
    <MyModal visible={visible} setVisible={setVisible}>
      <h1>{form.title}</h1>
      <form onSubmit={handleSubmit}>
        {form.fields.map((field) => (
          <div key={field.name}>
            <label>{field.label}</label>
            <input
              type={field.type}
              name={field.name}
              value={formData[field.name] || ''}
              onChange={handleChange}
              required={field.required}
            />
          </div>
        ))}
        <button type="submit">Создать</button>
        <button onClick={() => setVisible(false)}>Отмена</button>
      </form>
      
    </MyModal>
  );
};

export default GeneralForm;

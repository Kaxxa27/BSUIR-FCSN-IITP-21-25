import React from 'react';
import GeneralForm from './UI/GeneralForm/GeneralForm';

const CreateUserForm = ({ visible, setVisible }) => {
    
    const form = {
        fields: 
        [
            { name: 'username', label: 'Username:', type: 'text', required: true },
            { name: 'password', label: 'Пароль:', type: 'text', required: true },
            { name: 'email', label: 'Почта:', type: 'text', required: true },
            { name: 'age', label: 'Возраст:', type: 'number', required: true },
        ],
        modelURL: 'users',
        title:"Создать пользователя",
    };

  return (
    <GeneralForm form={form} visible={visible} setVisible={setVisible} />
  );
};

export default CreateUserForm;

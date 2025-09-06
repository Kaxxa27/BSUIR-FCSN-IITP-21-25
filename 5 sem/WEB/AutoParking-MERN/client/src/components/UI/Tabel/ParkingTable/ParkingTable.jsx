import React, { useContext, useState } from 'react';
import AuthContext from '../../../../context/authcontext';
import CreateParkingForm from '../../Forms/ParkingForm/CreateParkingForm';
import $api from '../../../../http/index';

const ParkingTable = ({ parkings = null }) => {
  const { isAuth } = useContext(AuthContext);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [updatedParking, setUpdatedParking] = useState(null);

  const handleEdit = (parking) => {
    setUpdatedParking(parking);
    setIsModalOpen(true);
  };

  const handleDelete = async (parkingId) => {
    try {
      await $api.delete(`/parkingspots/${parkingId}`);
    } catch (error) {
      console.error('Ошибка при удалении парковки:', error.response.data.message);
    }
  };

  return (
    <table>
      <thead>
        <tr>
          <th>Номер</th>
          <th>Цена</th>
          <th>Статус</th>
          <th>Владелец</th>
          <th>Машины</th>
          <th>Дата аренды</th>
          {isAuth && <th>Действия</th>}
        </tr>
      </thead>
      <tbody>
        {parkings.map((parking) => (
          <tr key={parking.id}>
            <td>{parking.number}</td>
            <td>{parking.price}</td>
            <td>{parking.is_busy ? 'Занято' : 'Свободно'}</td>
            <td>{parking.user ? parking.user : 'Отсутствует'}</td>
            <td>{parking.cars.count ? parking.cars : 'Отсутствуют'}</td>
            <td>{parking.date_of_rent ? parking.date_of_rent : ' Не Арендована'}</td>
            {isAuth && (
              <td>
                <button onClick={() => handleEdit(parking)}>Изменить</button>
                <button onClick={() => handleDelete(parking.id)}>Удалить</button>
              </td>
            )}
          </tr>
        ))}
      </tbody>
      
        {isModalOpen && (
          <CreateParkingForm visible={isModalOpen} setVisible={setIsModalOpen} isCreate = {false}  updatedParking={updatedParking} />
        )}  
    </table>
  );
};

export default ParkingTable;

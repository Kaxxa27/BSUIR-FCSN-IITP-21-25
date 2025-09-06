import React, { useContext } from 'react';
import AuthContext from '../context/authcontext';

const ParkingTable = ({ parkings }) => {
  const { isAuth } = useContext(AuthContext);

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
                {/* <button onClick={() => handleEdit(parking.id)}>Изменить</button>
                <button onClick={() => handleDelete(parking.id)}>Удалить</button> */}
                <button>Изменить</button>
                <button>Удалить</button>
              </td>
            )}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default ParkingTable;

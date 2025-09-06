import React, { useContext, useEffect, useState } from 'react';
import AuthContext from '../context/authcontext';
import ParkingTable from '../components/ParkingTable';
import CreateParkingForm from '../components/CreateParkingForm';
import $api from '../http/index';

const Catalog = () => {
  const { isAuth } = useContext(AuthContext);
  const [parkings, setParkings] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    const fetchParkings = async () => {
      try {
        const response = await $api.get('/parkingspots');
        setParkings(response.data);
      } catch (error) {
        console.error('Ошибка при загрузке парковок:', error.response.data.message);
      }
    };
    fetchParkings();
  }, []);

  return (
    <div>
      <h1>Каталог парковок</h1>
      {isAuth && (
      <>
        <button onClick={() => setIsModalOpen(true)}>Добавить новую парковку</button>
        {isModalOpen && (
          <CreateParkingForm visible={isModalOpen} setVisible={setIsModalOpen} />
        )}
      </>
    )}
      <ParkingTable parkings={parkings} />
    </div>
  );
};

export default Catalog;



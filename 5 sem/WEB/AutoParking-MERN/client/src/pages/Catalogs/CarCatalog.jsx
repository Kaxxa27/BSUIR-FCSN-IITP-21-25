import React, { useState, useEffect, useContext } from 'react';
import $api from '../../http/index';
import AuthContext from '../../context/authcontext';
import CreateCarForm from '../../components/UI/Forms/CarForm/CreateCarForm';
import UpdateCarForm from '../../components/UI/Forms/CarForm/UpdateCarForm';
import Select from 'react-select';

import cl from '../../styles/Catalogs/Catalog.module.css';

const CarCatalog = () => {
    const { isAuth } = useContext(AuthContext);
    const [isCreateModalOpen, setisCreateModalOpen] = useState(false);
    const [isUpdateModalOpen, setisUpdateModalOpen] = useState(false);
    const [selectedCars, setselectedCars] = useState(null);

    const [Cars, setCars] = useState([]);

    const [searchText, setSearchText] = useState('');

    useEffect(() => {
        loadCars(selectedOption);
    }, [searchText]);

    const [selectedOption, setSelectedOption] = useState(null);

    const options = [
        { value: '', label: 'Все' },
        { value: 'model', label: 'Модели' },
        { value: 'mark', label: 'Марка' },
        { value: 'license_plate', label: 'Номер' },
    ];

    const handleChange = (selectedOption) => {
        setSelectedOption(selectedOption);
        loadCars(selectedOption?.value);
    };

    const loadCars = async (sortField) => {
        try {
            const response = await $api.get('/cars');
            
            let filteredSpots = response.data;
            if (sortField) {
                filteredSpots = filteredSpots.sort((a, b) => a[sortField] - b[sortField]);
            }
            if (searchText !== '') {
                filteredSpots = filteredSpots.filter(car => car.license_plate.toString().includes(searchText));
            }
            setCars(filteredSpots);
        } catch (error) {
            console.error('Error loading parking Cars:', error);
        }
    };

    const deleteCar = async (id) => {
        try {
            await $api.delete(`/cars/${id}`);
            setCars(Cars.filter((Car) => Car._id !== id));
        } catch (error) {
            console.error('Error deleting parking Car:', error);
        }
    };

    const editCar = (Car) => {
        setselectedCars(Car);
        setisUpdateModalOpen(true);
    };

    return (
        <div className={cl.mainContainer}>
            <h1>Cars Catalog</h1>
            <div>
                <Select
                        value={selectedOption}
                        onChange={handleChange}
                        options={options}
                        placeholder="Выберите поле"
                    />
            </div>
            <br/>
            <br/>
            <input
                type="text"
                value={searchText}
                onChange={(e) => setSearchText(e.target.value)}
                placeholder="Поиск по номеру"
                className={cl.searchInput}
            />
            <br/>

            {isAuth && (
                <>
                    <button onClick={() => setisCreateModalOpen(true)}>Добавить машину</button>
                    {isCreateModalOpen && (
                        <CreateCarForm
                            visible={isCreateModalOpen}
                            loadCars={() => loadCars()}
                            setVisible={setisCreateModalOpen}
                        />
                    )}
                    {selectedCars && (
                        <UpdateCarForm
                            loadCars={() => loadCars()}
                            Car={selectedCars}
                            visible={isUpdateModalOpen}
                            setVisible={setisUpdateModalOpen}
                        />
                    )}
                </>
            )}
            {Cars.map((Car) => (
                <div key={Car._id} className={cl.Block}>
                    <div className={cl.infoBlock}>
                        <div>Model: {Car.model}</div>
                        <div>Mark: {Car.mark}</div>
                        <div>License Plat: {Car.license_plate}</div>
                    </div>
                    {isAuth &&
                        <div>
                            <button onClick={() => editCar(Car)}>Edit</button>
                            <button onClick={() => deleteCar(Car._id)}>Delete</button>
                        </div>
                    }
                </div>
            ))}
        </div>
    );
};

export default CarCatalog;

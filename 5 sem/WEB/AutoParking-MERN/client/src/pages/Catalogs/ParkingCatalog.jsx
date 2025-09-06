import React, { useState, useEffect, useContext } from 'react';
import $api from '../../http/index';
import AuthContext from '../../context/authcontext';
import CreateParkingForm from '../../components/UI/Forms/ParkingForm/CreateParkingForm';
import UpdateParkingForm from '../../components/UI/Forms/ParkingForm/UpdateParkingForm';
import Select from 'react-select';

import cl from '../../styles/Catalogs/Catalog.module.css';

const ParkingCatalogs = () => {
    const { isAuth } = useContext(AuthContext);
    const [isCreateModalOpen, setisCreateModalOpen] = useState(false);
    const [isUpdateModalOpen, setisUpdateModalOpen] = useState(false);
    const [selectedParkingSpot, setSelectedParkingSpot] = useState(null);

    const [parkingSpots, setParkingSpots] = useState([]);

    const [searchText, setSearchText] = useState('');
    
    useEffect(() => {
        loadParkingSpots(selectedOption);
    }, [searchText]);


    const [selectedOption, setSelectedOption] = useState(null);

    const options = [
        { value: '', label: 'Все' },
        { value: 'number', label: 'Номеру' },
        { value: 'price', label: 'Цене' },
    ];

    const handleChange = (selectedOption) => {
        setSelectedOption(selectedOption);
        loadParkingSpots(selectedOption?.value);
    };

    const loadParkingSpots = async (sortField) => {
        try {
            const response = await $api.get('/parkingSpots');
            let filteredSpots = response.data;
            if (sortField) {
                filteredSpots = filteredSpots.sort((a, b) => a[sortField] - b[sortField]);
            }
            if (searchText !== '') {
                filteredSpots = filteredSpots.filter(spot => spot.number.toString().includes(searchText));
            }
            setParkingSpots(filteredSpots);
        } catch (error) {
            console.error('Error loading parking spots:', error);
        }
    }

    const deleteParkingSpot = async (id) => {
        try {
            await $api.delete(`/parkingSpots/${id}`);
            setParkingSpots(parkingSpots.filter((spot) => spot._id !== id));
        } catch (error) {
            console.error('Error deleting parking spot:', error);
        }
    };

    const editParkingSpot = (spot) => {
        setSelectedParkingSpot(spot);
        setisUpdateModalOpen(true);
    };

    return (
        <div className={cl.mainContainer}>
            <h1>Parking Catalog</h1>

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
                    <button onClick={() => setisCreateModalOpen(true)}>Добавить новую парковку</button>
                    {isCreateModalOpen && (
                        <CreateParkingForm
                            visible={isCreateModalOpen}
                            loadParkingSpots={() => loadParkingSpots()}
                            setVisible={setisCreateModalOpen}
                        />
                    )}
                    {selectedParkingSpot && (
                        <UpdateParkingForm
                            loadParkingSpots={() => loadParkingSpots()}
                            parkingSpot={selectedParkingSpot}
                            visible={isUpdateModalOpen}
                            setVisible={setisUpdateModalOpen}
                        />
                    )}
                </>
            )}
            {parkingSpots.map((spot) => (
                <div key={spot._id} className={cl.Block}>
                    <div className={cl.infoBlock}>
                        <div>Number: {spot.number}</div>
                        <div>Price: {spot.price}</div>
                    </div>
                    {isAuth &&
                        <div>
                            <button onClick={() => editParkingSpot(spot)}>Edit</button>
                            <button onClick={() => deleteParkingSpot(spot._id)}>Delete</button>
                        </div>
                    }
                </div>
            ))}
        </div>
    );
};

export default ParkingCatalogs;

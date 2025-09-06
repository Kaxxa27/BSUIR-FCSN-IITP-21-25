import React, { useState, useEffect, useContext } from 'react';
import $api from '../../http/index';
import AuthContext from '../../context/authcontext';
import CreateUserForm from '../../components/UI/Forms/UserForm/CreateUserForm';
import UpdateUserForm from '../../components/UI/Forms/UserForm/UpdateUserForm';
import Select from 'react-select';

import cl from '../../styles/Catalogs/Catalog.module.css';

const UserCatalog = () => {
    const { isAuth } = useContext(AuthContext);
    const [isCreateModalOpen, setisCreateModalOpen] = useState(false);
    const [isUpdateModalOpen, setisUpdateModalOpen] = useState(false);
    const [selectedUsers, setselectedUsers] = useState(null);

    const [Users, setUsers] = useState([]);

    const [searchText, setSearchText] = useState('');

    useEffect(() => {
        loadUsers(selectedOption);
    }, [searchText]);

    const [selectedOption, setSelectedOption] = useState(null);

    const options = [
        { value: '', label: 'Все' },
        { value: 'username', label: 'Имени' },
        { value: 'age', label: 'Возрасту' },
    ];

    const handleChange = (selectedOption) => {
        setSelectedOption(selectedOption);
        loadUsers(selectedOption?.value);
    };


    const loadUsers = async (sortField) => {
        try {
            const response = await $api.get('/users');

            let filteredSpots = response.data;
            if (sortField) {
                filteredSpots = filteredSpots.sort((a, b) => a[sortField] - b[sortField]);
            }
            if (searchText !== '') {
                filteredSpots = filteredSpots.filter(user => user.username.toString().includes(searchText));
            }
            setUsers(filteredSpots);
        } catch (error) {
            console.error('Error loading parking users:', error);
        }
    };

    const deleteUser = async (id) => {
        try {
            await $api.delete(`/users/${id}`);
            setUsers(Users.filter((user) => user._id !== id));
        } catch (error) {
            console.error('Error deleting parking user:', error);
        }
    };

    const editUser = (user) => {
        setselectedUsers(user);
        setisUpdateModalOpen(true);
    };

    return (
        <div className={cl.mainContainer}>
            <h1>Users Catalog</h1>
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
                placeholder="Поиск по имени"
                className={cl.searchInput}
            />
            <br/>

            {isAuth && (
                <>
                    <button onClick={() => setisCreateModalOpen(true)}>Добавить нового пользователя</button>
                    {isCreateModalOpen && (
                        <CreateUserForm
                            visible={isCreateModalOpen}
                            loadUsers={() => loadUsers()}
                            setVisible={setisCreateModalOpen}
                        />
                    )}
                    {selectedUsers && (
                        <UpdateUserForm
                            loadUsers={() => loadUsers()}
                            user={selectedUsers}
                            visible={isUpdateModalOpen}
                            setVisible={setisUpdateModalOpen}
                        />
                    )}
                </>
            )}
            {Users.map((user) => (
                <div key={user._id} className={cl.Block}>
                    <div className={cl.infoBlock}>
                        <div>Username: {user.username}</div>
                        <div>Email: {user.email}</div>
                        <div>Password: {user.password}</div>
                        <div>Age: {user.age}</div>
                    </div>
                    {isAuth &&
                        <div>
                            <button onClick={() => editUser(user)}>Edit</button>
                            <button onClick={() => deleteUser(user._id)}>Delete</button>
                        </div>
                    }
                </div>
            ))}
        </div>
    );
};

export default UserCatalog;

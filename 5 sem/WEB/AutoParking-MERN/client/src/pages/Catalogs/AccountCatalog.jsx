import React, { useState, useEffect, useContext } from 'react';
import $api from '../../http/index';
import AuthContext from '../../context/authcontext';
import CreateAccountForm from '../../components/UI/Forms/AccountForm/CreateAccountForm';
import UpdateAccountForm from '../../components/UI/Forms/AccountForm/UpdateAccountForm';
import Select from 'react-select';

import cl from '../../styles/Catalogs/Catalog.module.css';

const AccountCatalog = () => {
    const { isAuth } = useContext(AuthContext);
    const [isCreateModalOpen, setisCreateModalOpen] = useState(false);
    const [isUpdateModalOpen, setisUpdateModalOpen] = useState(false);
    const [selectedAccounts, setselectedAccounts] = useState(null);

    const [Accounts, setAccounts] = useState([]);

    const [searchText, setSearchText] = useState('');

    useEffect(() => {
        loadAccounts(selectedOption);
    }, [searchText]);

    const [selectedOption, setSelectedOption] = useState(null);

    const options = [
        { value: '', label: 'Все' },
        { value: 'amount', label: 'Кол-ву денег' },
    ];

    const handleChange = (selectedOption) => {
        setSelectedOption(selectedOption);
        loadAccounts(selectedOption?.value);
    };

    const loadAccounts = async (sortField) => {
        try {
            const response = await $api.get('/accounts');

            let filteredSpots = response.data;
            if (sortField) {
                filteredSpots = filteredSpots.sort((a, b) => a[sortField] - b[sortField]);
            }
            if (searchText !== '') {
                filteredSpots = filteredSpots.filter(account => account.amount.toString().includes(searchText));
            }
            setAccounts(filteredSpots);
        } catch (error) {
            console.error('Error loading parking Accounts:', error);
        }
    };

    const deleteAccount = async (id) => {
        try {
            await $api.delete(`/accounts/${id}`);
            setAccounts(Accounts.filter((Account) => Account._id !== id));
        } catch (error) {
            console.error('Error deleting parking Account:', error);
        }
    };

    const editAccount = (Account) => {
        setselectedAccounts(Account);
        setisUpdateModalOpen(true);
    };

    return (
        <div className={cl.mainContainer}>
            <h1>Accounts Catalog</h1>
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
                placeholder="Поиск по счету"
                className={cl.searchInput}
            />
            <br/>

            {isAuth && (
                <>
                    <button onClick={() => setisCreateModalOpen(true)}>Добавить счет</button>
                    {isCreateModalOpen && (
                        <CreateAccountForm
                            visible={isCreateModalOpen}
                            loadAccounts={() => loadAccounts()}
                            setVisible={setisCreateModalOpen}
                        />
                    )}
                    {selectedAccounts && (
                        <UpdateAccountForm
                            loadAccounts={() => loadAccounts()}
                            Account={selectedAccounts}
                            visible={isUpdateModalOpen}
                            setVisible={setisUpdateModalOpen}
                        />
                    )}
                </>
            )}
            {Accounts.map((Account) => (
                <div key={Account._id} className={cl.Block}>
                    <div className={cl.infoBlock}>
                        <div>Amount: {Account.amount}</div>
                    </div>
                    {isAuth &&
                        <div>
                            <button onClick={() => editAccount(Account)}>Edit</button>
                            <button onClick={() => deleteAccount(Account._id)}>Delete</button>
                        </div>
                    }
                </div>
            ))}
        </div>
    );
};

export default AccountCatalog;

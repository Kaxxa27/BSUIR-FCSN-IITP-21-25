import React, { useState, useEffect, useContext } from 'react';
import AuthContext from '../../../context/authcontext';
import GeneralTable  from '../../UI/GeneralTable/GeneralTable';
import $api from '../../../http/index';

const GeneralCatalog = ({ catalogTitle, modelName, catalogURL, tableFields, CreateFormComponent }) => {

    const { isAuth } = useContext(AuthContext);
    const [data, setData] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await $api.get(`/${catalogURL}`);
                setData(response.data);

            } catch (error) {
                console.error(`Error fetching data:`, error.response.data.message);
            }
        };
        fetchData();
    }, [catalogURL]);

    return (
        <div>
            <h1>{`${catalogTitle}`}</h1>
            {isAuth && (
                <>
                    <button onClick={() => setIsModalOpen(true)}>Добавить новую запись</button>
                    {isModalOpen && (
                        <CreateFormComponent
                            modelURL={catalogURL}
                            fields={tableFields}
                            visible={isModalOpen}
                            setVisible={setIsModalOpen}
                        />
                    )}
                </>
            )}
            <GeneralTable  data={data} fields={tableFields} />
        </div>
    );
};

export default GeneralCatalog;

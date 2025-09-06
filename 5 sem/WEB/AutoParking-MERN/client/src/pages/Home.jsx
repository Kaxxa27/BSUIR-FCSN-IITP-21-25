import React from 'react';
import IpComponent from '../components/API/IpAPI';
import CatFactComponent from '../components/API/FactAboutCatsAPI';

import cl from '../styles/General.module.css'

const Home = () => {
    return (
        <div className={cl.container}>

            <h1 className={cl.info} >Главная страница</h1>

            <div className={cl.Block}>
                <IpComponent />
            </div>
            <div className={cl.Block}>
                <CatFactComponent />
            </div>


        </div>
    );
};

export default Home;

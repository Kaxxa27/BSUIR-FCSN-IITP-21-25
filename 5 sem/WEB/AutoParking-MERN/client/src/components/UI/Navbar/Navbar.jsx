import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import AuthContext from '../../../context/authcontext';
import './Navbar.module.css';


const Navbar = () => {
  const {isAuth} = useContext(AuthContext);
  return (
    <nav>
      <ul>
        <li><Link to="/">Главная</Link></li>
        <li><Link to="/parking-catalog">Каталог парковок</Link></li>
        <li><Link to="/users-catalog">Каталог пользователей</Link></li>
        <li><Link to="/cars-catalog">Каталог машин</Link></li>
        <li><Link to="/accounts-catalog">Счета</Link></li>
        <li><Link to="/about">О нас</Link></li>
        <li><Link to="/privacy-policy">Политика Конфиденциальности</Link></li>
        {!isAuth && 
          <div>
            <li><Link to="/login">Войти</Link></li>
            <li><Link to="/registration">Зарегистрироваться</Link></li>
          </div>
        }
        {isAuth &&
          <li><Link to="/logout">Выйти</Link></li>
        }
      </ul>
    </nav>
  );
};

export default Navbar;

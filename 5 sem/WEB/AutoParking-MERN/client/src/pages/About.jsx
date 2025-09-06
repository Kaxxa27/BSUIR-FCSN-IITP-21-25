import React from 'react';
import cl from '../styles/General.module.css'

const About = () => {

    return (
        <div className = {cl.container}>

            <div style={{ fontWeight: 'bold', fontSize: '24px' }}>
                <div className={cl.Block}>
                    <div>Информация о компании:</div>
                    <div>
                        Автостоянка <abbr title="ООО Автопарковка Kaxxa">ByKaxxa</abbr> - это современная сеть автомобильных парковок, предоставляющая высококачественные услуги
                        хранения и обслуживания транспортных средств.
                        Компания была основана в 2017 году и с тех пор стремится предоставлять удобные и
                        безопасные места для парковки автомобилей, мотоциклов и других транспортных средств.
                    </div>
                </div>

                <div className={cl.Block}>
                    <div>История по годам:</div>
                    <div>
                        <div>
                            <div><strong>2017</strong>: Основание компании "ByKaxxa". Постройка первой технологичной автостоянки.</div>
                            <div><strong>2019</strong>: Запуск нашей первой крупной автостоянки в центре Минска.</div>
                            <div><strong>2023</strong>: Улучшение технологий безопасности и предоставления услуг по аренде.</div>
                        </div>
                    </div>
                </div>

                <div className={cl.Block}>
                    <div>Реквизиты:</div>
                    <dive>
                        <div>
                            <div>Юридический адрес: ул. Гикало, 9, Минск, Беларусь</div>
                            <div>УНП: 1234567890</div>
                            <div>ГЛН: 0987654321</div>
                            <div>Контактный телефон: +375 (29) 777-77-87</div>
                            <div>Электронная почта: info@parking-bykaxxa.com</div>
                        </div>
                    </dive>
                </div>

            </div>

            
        </div >
    )
}

export default About;

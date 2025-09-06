// прототипное наследование 

function Car(model, maxSpeed){
    let _model = model;
    let _maxSpeed = maxSpeed;

    this.model = (model) => {
        if(model === undefined)
            return _model;
        else _model = model
    }

    this.maxSpeed = (maxSpeed) => {
        if(maxSpeed === undefined)
            return _maxSpeed;
        else _maxSpeed = maxSpeed
    }

    this.toString = () => {
        return `Model: ${_model} -> maxSpeed: ${_maxSpeed}`
    }

    this.toString = isFast(this.maxSpeed);
}

function Mersedes(model, maxSpeed, price)
{
    this.__proto__ = new Car(model, maxSpeed);

    let _price = price;
    
    this.price = (price) => {
        if(price === undefined)
            return _price;
        else _price = price
    }

    toString = () => {
        return `Model: ${_model} -> maxSpeed: ${_maxSpeed} -> ${_price} `
    }

}

// Декоратор для проверки максимальной скорости
function isFast(func) {
    function wrapper(params) {
        const maxSpeed = this.maxSpeed();
        const isFast = maxSpeed > 200;

        console.log(`Is it fast? ${isFast ? 'Yes' : 'No'}`);
        return func.apply(this, params);
    }
    
    return wrapper;
}



const myCar = new Mersedes('S-Class', 250, 50000);

console.log(myCar.model());      // Output: S-Class
console.log(myCar.maxSpeed());   // Output: 250
console.log(myCar.price());   // Output: 50000
console.log(myCar.toString());      // Output: Model: S-Class -> maxSpeed: 250

myCar.model('E-Class');
myCar.maxSpeed(199);
myCar.price(60000);

console.log(myCar.model());      // Output: E-Class
console.log(myCar.maxSpeed());   // Output: 300
console.log(myCar.toString());      // Output: Model: E-Class -> maxSpeed: 300
console.log(myCar.price());      // Output: 60000


// Classes 
// class Car {
//     constructor(model, maxSpeed) {
//         this._model = model;
//         this._maxSpeed = maxSpeed;
//     }

//     model(model) {
//         if (model === undefined)
//             return this._model;
//         else
//             this._model = model;
//     }

//     maxSpeed(maxSpeed) {
//         if (maxSpeed === undefined)
//             return this._maxSpeed;
//         else 
//             this._maxSpeed = maxSpeed;
//     }

//     toString = () => {
//         return `Model: ${this.model()} -> maxSpeed: ${this.maxSpeed()}`
//     }

//     toString = isFast(this.maxSpeed);
// }


// class Mersedes extends Car {
//     constructor(model, maxSpeed, price) {
//         super(model, maxSpeed);
//         this._price = price;
//     }

//     price(price) {
//         if (price === undefined)
//             return this._price;
//         else
//            this._price = price;
//     }

//     toString = () => {
//         return `Model: ${this.model()} -> maxSpeed: ${this.maxSpeed()} -> ${this.price()} `
//     }
// }

// // Декоратор для проверки максимальной скорости
// function isFast(func) {
//     function wrapper(params) {
//         const maxSpeed = this.maxSpeed();
//         const isFast = maxSpeed > 200;

//         console.log(`Is it fast? ${isFast ? 'Yes' : 'No'}`);
//         return func.apply(this, params);
//     }
    
//     return wrapper;
// }

// //Doctor.prototype.specialization = log_decorator(Doctor.prototype.specialization);


// const myCar = new Mersedes('S-Class', 250, 50000);

// console.log(myCar.model());      // Output: S-Class
// console.log(myCar.maxSpeed());   // Output: 250
// console.log(myCar.price());   // Output: 50000
// console.log(myCar.toString());      // Output: Model: S-Class -> maxSpeed: 250

// myCar.model('E-Class');
// myCar.maxSpeed(199);
// myCar.price(60000);

// console.log(myCar.model());      // Output: E-Class
// console.log(myCar.maxSpeed());   // Output: 300
// console.log(myCar.toString());      // Output: Model: E-Class -> maxSpeed: 300
// console.log(myCar.price());      // Output: 60000
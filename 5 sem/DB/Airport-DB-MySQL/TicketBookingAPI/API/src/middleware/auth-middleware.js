const jwt = require('jsonwebtoken');
const ApiError = require('../exceptions/api-error');

const authMiddleware = (req, res, next) => {
    const authorization = req.header('Authorization');

    if (!authorization) {
        return next(ApiError.UnauthorizedError());
    }

    const token = authorization.split(' ')[1];

    jwt.verify(token, process.env.SECRET_KEY, (err, user) => {
        if (err) {
            return next(ApiError.UnauthorizedError());
        }

        req.user = user;
        next();
    });
};

module.exports = authMiddleware;
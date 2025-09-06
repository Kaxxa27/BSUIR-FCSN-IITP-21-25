const ApiError = require('../exceptions/api-error');

const staffMiddleware = (req, res, next) => {
    if (!req.user.is_staff) {
        return next(ApiError.ForbiddenError());
    }

    next();
};

module.exports = staffMiddleware;
const ApiError = require('../exceptions/api-error');

module.exports = (err, req, res, next) => {
    console.error(err);

    if(err instanceof ApiError){
        return res.status(err.status).json({ message: err.message, errors: err.errors});
    }

    if(err.sql){
        return res.status(400).json({
            message: err.sqlMessage || '',
            state: err.sqlState || '',
            sql: err.sql
        });
    }

    res.status(500).end();
};
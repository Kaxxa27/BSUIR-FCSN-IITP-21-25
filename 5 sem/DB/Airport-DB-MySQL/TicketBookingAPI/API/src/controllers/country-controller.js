const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const CountryController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM country');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM country WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("country not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name, code } = req.body;

        try {
            const query = `
                INSERT INTO country(name, code)
                VALUES (?, ?)
            `;

            await pool.query(query, [name, code]);

            res.status(201).json({ message: 'country created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {name, code } = req.body;

        try {
            const query = `
                UPDATE country
                SET name = ?, code = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, code, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("country not found"));
            }

            res.status(200).json({ message: 'country updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM country WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("country not found"));
            }

            res.status(200).json({ message: 'country deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = CountryController;
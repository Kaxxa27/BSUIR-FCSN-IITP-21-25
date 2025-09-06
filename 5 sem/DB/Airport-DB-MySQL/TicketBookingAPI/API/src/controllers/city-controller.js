const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const CityController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM city');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM city WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("city not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name, country_id } = req.body;

        try {
            const query = `
                INSERT INTO city(name, country_id)
                VALUES (?, ?)
            `;

            await pool.query(query, [name, country_id]);

            res.status(201).json({ message: 'city created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {name, country_id } = req.body;

        try {
            const query = `
                UPDATE city
                SET name = ?, country_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, country_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("city not found"));
            }

            res.status(200).json({ message: 'city updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM city WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("city not found"));
            }

            res.status(200).json({ message: 'city deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = CityController;
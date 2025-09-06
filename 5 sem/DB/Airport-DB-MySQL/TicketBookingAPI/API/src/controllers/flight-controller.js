const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const FlightController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM flight');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM flight WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("flight not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { flight_number, airport_id, plane_id, airline_id } = req.body;

        try {
            const query = `
                INSERT INTO flight(flight_number, airport_id, plane_id, airline_id)
                VALUES (?, ?, ?, ?)
            `;

            await pool.query(query, [flight_number, airport_id, plane_id, airline_id]);

            res.status(201).json({ message: 'flight created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { flight_number, airport_id, plane_id, airline_id } = req.body;

        try {
            const query = `
                UPDATE flight
                SET flight_number = ?, airport_id = ?, plane_id = ?, airline_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [flight_number, airport_id, plane_id, airline_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("flight not found"));
            }

            res.status(200).json({ message: 'flight updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM flight WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("flight not found"));
            }

            res.status(200).json({ message: 'flight deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = FlightController;
const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const ScheduleController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM schedule');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM schedule WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("schedule not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { expected_departure, expected_arrival, 
            real_departure, real_arrival, flight_id } = req.body;

        try {
            const query = `
                INSERT INTO schedule(expected_departure, expected_arrival, 
                    real_departure, real_arrival, flight_id)
                VALUES (?, ?, ?, ?, ?)
            `;

            await pool.query(query, [expected_departure, expected_arrival, 
                real_departure, real_arrival, flight_id]);

            res.status(201).json({ message: 'schedule created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { expected_departure, expected_arrival, 
            real_departure, real_arrival, flight_id } = req.body;

        try {
            const query = `
                UPDATE schedule
                SET expected_departure = ?, expected_arrival = ?, 
                real_departure = ?, real_arrival = ?, flight_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [expected_departure, expected_arrival, 
                real_departure, real_arrival, flight_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("schedule not found"));
            }

            res.status(200).json({ message: 'schedule updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM schedule WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("schedule not found"));
            }

            res.status(200).json({ message: 'schedule deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = ScheduleController;
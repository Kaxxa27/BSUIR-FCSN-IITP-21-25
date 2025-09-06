const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const ScriptsController = {
    someWhere: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                SELECT * FROM ticket t
                INNER JOIN flight f ON t.flight_id = f.id
                INNER JOIN airport a ON f.airport_id = a.id
                INNER JOIN city c ON a.city_id = c.id
                WHERE price > 500 AND code_IATA = 'MSQ'
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },
    having: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                SELECT flight_number, sum(price) FROM ticket t
                INNER JOIN flight f ON t.flight_id = f.id
                GROUP BY f.flight_number
                HAVING sum(price) BETWEEN 1000 AND 5000
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },
    aggregateFunc: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                SELECT flight_number, 
                sum(price) as sum, 
                count(t.id) as count, 
                min(price) as min, 
                max(price) as max, 
                avg(price) as a 
                FROM ticket t
                INNER JOIN flight f ON t.flight_id = f.id
                GROUP BY flight_number
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },
    allSumOfTicketPrice: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                SELECT flight_number, sum(price) FROM ticket t
                INNER JOIN flight f ON t.flight_id = f.id
                INNER JOIN airport a ON f.airport_id = a.id
                INNER JOIN city c ON a.city_id = c.id
                GROUP BY flight_number
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },
    leadLagEtc: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                SELECT flight_number, price, 
                LEAD(price) over (partition by flight_number) as \`lead+1\`,
                LEAD(price, 3) over (partition by flight_number) as \`lead+3\`,
                LAG(price) over (partition by flight_number) as \`lag-1\`,
                LAG(price, 2) over (partition by flight_number) as \`lag-2\`,
                FIRST_VALUE(price) over (partition by flight_number) as \`first\`,
                LAST_VALUE(price) over (partition by flight_number) as \`last\`
                FROM ticket t
                INNER JOIN flight f ON t.flight_id = f.id
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },
    theMostPopularNationality: async (req, res, next) => {
        try {
            const [rows] = await pool.query(`
                WITH num_ntl AS (
                    SELECT flight_number, nationality_id, COUNT(*) as num_of_n
                    FROM ticket t 
                    INNER JOIN flight f ON f.id = t.flight_id 
                    INNER JOIN passport p ON p.id = t.passport_id
                    INNER JOIN nationality n ON n.id = p.nationality_id
                    GROUP BY flight_number, nationality_id
                    ORDER BY flight_number
                )
            
                SELECT flight_number, name as popular_nat, num_of_n
                FROM (
                    SELECT flight_number, name, num_of_n, 
                    dense_rank() over (partition by flight_number order by num_of_n desc) as nat_rank,
                    row_number() over (partition by flight_number order by num_of_n desc) as row_num
                    FROM num_ntl
                    INNER JOIN nationality n ON n.id = num_ntl.nationality_id
                ) as res
                WHERE row_num = 1
            `);
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

}
module.exports = ScriptsController;
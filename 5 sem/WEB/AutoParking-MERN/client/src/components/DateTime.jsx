import React, { useState, useEffect } from 'react';

const DateTimeComponent = ({ userTimeZone }) => {
  const [dataModifiedDate, setDataModifiedDate] = useState('');
  const [error, setError] = useState('');

  useEffect(() => {
    getDataModifiedDate();
  }, [userTimeZone]);

  const getDataModifiedDate = () => {
    try {
      const now = new Date();
      if (!isNaN(now.getTime())) {
        const formattedDate = new Intl.DateTimeFormat('en-US', {
          timeZone: userTimeZone,
          dateStyle: 'full',
          timeStyle: 'long',
        }).format(now);
        setDataModifiedDate(formattedDate);
      } else {
        throw new Error('Invalid date');
      }
    } catch (error) {
      setError(`Error: ${error.message}`);
    }
  };

  return (
    <div>
      {dataModifiedDate && (
        <p>
          {`Data Modification Date (${userTimeZone}): ${dataModifiedDate}`}
        </p>
      )}
      {error && <p>{error}</p>}
    </div>
  );
};

export default DateTimeComponent;

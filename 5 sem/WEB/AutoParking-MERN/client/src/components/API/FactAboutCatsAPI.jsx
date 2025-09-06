import React, { useState, useEffect } from 'react';

const CatFactComponent = function() {
  const [catFact, setCatFact] = useState('');
  const [error, setError] = useState(null);

  useEffect(() => {
    getCatFact();
  }, []);

  const getCatFact = async () => {
    try {
      const response = await fetch('https://catfact.ninja/fact');

      if (response.ok) {
        const data = await response.json();
        setCatFact(data.fact);
      } else {
        throw new Error(`Error: ${response.status}`);
      }
    } catch (error) {
      setError(`Failed to fetch cat fact: ${error.message}`);
    }
  };

  return (
    <div>
      <h2>Random Cat Fact:</h2>
      {error ? <p>{error}</p> : <p>{catFact}</p>}
    </div>
  );
};

export default CatFactComponent;

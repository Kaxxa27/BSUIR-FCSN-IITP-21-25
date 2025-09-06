import React from 'react';

const GeneralTable = ({ data, fields }) => {
  return (
    <table>
      <thead>
        <tr>
          {fields.map((field) => (
            <th key={field.name}>{field.label}</th>
          ))}
        </tr>
      </thead>
      <tbody>
        {data.map((item) => (
          <tr key={item.id}>
            {fields.map((field) => (
              <td key={field.name}>{item[field.name]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default GeneralTable;


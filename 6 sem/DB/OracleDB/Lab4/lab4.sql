DECLARE
  v_col      VARCHAR2(8000);
  output_cursor SYS_REFCURSOR;
  
   json_create_t1 CLOB := '{
      "query_type": "CREATE TABLE",
      "table": "t1",
      "columns": [
         {
            "name": "id", "type": "NUMBER"
         },
         {
            "name": "num", "type": "NUMBER"
         },
         {
            "name": "val", "type": "VARCHAR2(200)"
         }
      ],
      "primary_keys": ["id"]
   }';
   

   json_create_t2 CLOB := '{
      "query_type": "CREATE TABLE",
      "table": "t2",
      "columns": [
         {
            "name": "id", "type": "NUMBER"
         },
         {
            "name": "num", "type": "NUMBER"
         },
         {
            "name": "val", "type": "VARCHAR2(200)"
         },
         {
            "name": "t1_k", "type": "NUMBER"
         }
         
      ],
      "primary_keys": ["id"],
      "foreign_keys": [{"field": "id", "table": "t1", "ref_field": "id"}]
   }';
   
   json_select_t1_t2 CLOB := '
   {
  "query_type": "SELECT",
  "columns": ["*"],
  "tables": ["t1"],
  "join_block":
  [
    "RIGHT",
    "t2",
    "t2.t1_k = t1.id"
  ],
  "filter_conditions": [
    {
      "condition_type": "included",
      "condition": {
        "query_type": "SELECT",
        "columns": ["id"],
        "tables": ["t2"],
        "filter_conditions": [
          {"condition": "val like ''%a%''", "operator": "AND"},
          {
            "condition": "num between 2 and 4",
            "operator": "AND"
          }
        ],
        "operator": "IN",
        "search_col": "t1.id"
      },
      "operator": "AND"
    }
  ]
}

   ';
   
   json_test_join_select CLOB := '{
  "query_type": "SELECT",
  "columns": [
    "*"
  ],
  "tables": [
    "aaa"
  ],
  "join_block": [],
  "filter_conditions": [
    {
      "condition_type": "included",
      "condition": {
        "query_type": "SELECT",
        "column": "id",
        "tables": [
          "bbb"
        ],
        "filter_conditions": [
                  {
            "condition": "id between 3 and 5",
            "operator": "AND"
          },
          {
            "condition": "name like ''%''",
            "operator": "AND"
          }

        ],
        "operator": "NOT IN",
        "search_col": "id"
      },
      "operator": "AND"
    }
  ]
}';
   
   
   -- DROP TABLE t1;
BEGIN

     --parse_json_proc(json_test_join_select, output_cursor);
   -- parse_json_proc(json_create_t1, output_cursor);
  --  parse_json_proc(json_create_t2, output_cursor);
  parse_json_proc(json_select_t1_t2, output_cursor);
  
END;-- 

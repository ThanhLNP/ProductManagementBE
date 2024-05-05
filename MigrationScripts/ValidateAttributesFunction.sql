DROP FUNCTION IF EXISTS validate_attributes;

create function validate_attributes(p_allowed jsonb, p_to_check jsonb)
  returns boolean
as
$$
   select p_allowed ?& (select array_agg(k) from jsonb_object_keys(p_to_check) as t(k));
$$
language sql;
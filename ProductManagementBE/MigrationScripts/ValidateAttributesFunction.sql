DROP FUNCTION IF EXISTS public.validate_attributes;

CREATE OR REPLACE FUNCTION validate_attributes(p_allowed jsonb, p_to_check jsonb)
returns boolean
as
$$
BEGIN
   RETURN (
     SELECT p_allowed ?& array_agg(key)
     FROM jsonb_object_keys(p_to_check) AS t(key)
   );
END;
$$
language plpgsql;

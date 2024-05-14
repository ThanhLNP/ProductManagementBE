DROP FUNCTION IF EXISTS public.validate_category_trg;

CREATE OR REPLACE FUNCTION validate_category_trg()
returns trigger
as
$$
declare
   l_allowed jsonb;
   l_valid   boolean;
begin

   select c."AllowedAttributes"
      into l_allowed
   from "Category" c
   WHERE c."Id" = NEW."CategoryId";

   l_valid := validate_attributes(l_allowed, NEW."Attributes");

   IF NOT l_valid THEN 
     RAISE EXCEPTION 'Some attributes are not allowed for that category';
   end if;

   return new;
end;
$$
language plpgsql;

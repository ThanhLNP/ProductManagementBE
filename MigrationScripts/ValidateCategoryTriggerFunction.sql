DROP FUNCTION IF EXISTS validate_category_trg;

create function validate_category_trg()
  returns trigger
as
$$
declare
   l_allowed jsonb;
   l_valid   boolean;
begin

   select allowed_attributes 
      into l_allowed
   from category
   where id = new.category_id;

   l_valid := validate_attributes(l_allowed, new.attributes);
   if l_valid = false then 
     raise 'some attributes are not allowed for that category';
   end if;
   return new;
end;
$$
language plpgsql;
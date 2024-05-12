import { FormGroup, ValidationErrors } from '@angular/forms';

export function getFormValidationErrors(form: FormGroup) {
  const result: any[] = [];
  Object.keys(form.controls).forEach((key) => {
    const controlErrors: ValidationErrors = form.get(key)?.errors!;
    if (controlErrors) {
      Object.keys(controlErrors).forEach((keyError) => {
        result.push({
          control: key,
          error: keyError,
          value: controlErrors[keyError],
        });
      });
    }
  });

  return result;
}

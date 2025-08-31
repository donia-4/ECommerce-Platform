import { useState } from "react";
import "./account.css";

export default function Account() {
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    address: "",
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
  });

  const [errors, setErrors] = useState({});
  const [submitted, setSubmitted] = useState(false);
  const [successMessage, setSuccessMessage] = useState(""); 

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleBlur = (e) => {
    const { name, value } = e.target;
    const fieldError = runFieldValidation(name, value, form);

    setErrors((prev) => ({
      ...prev,
      [name]: fieldError,
    }));
  };

  const runFieldValidation = (name, value, allValues) => {
    switch (name) {
      case "firstName":
        if (!value.trim()) return "First name is required";
        if (value.length < 2) return "First name must be at least 2 characters";
        if (!/^[A-Za-z]+$/.test(value))
          return "First name should contain only letters";
        break;

      case "lastName":
        if (!value.trim()) return "Last name is required";
        if (value.length < 2) return "Last name must be at least 2 characters";
        if (!/^[A-Za-z]+$/.test(value))
          return "Last name should contain only letters";
        break;

      case "email":
        if (!value.trim()) return "Email is required";
        if (!/^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/.test(value))
          return "Invalid email format";
        break;

      case "address":
        if (!value.trim()) return "Address is required";
        if (value.length < 10) return "Address must be at least 10 characters";
        break;

      case "currentPassword":
        if (!value.trim()) return "Current password is required";
        break;

      case "newPassword":
        if (!value.trim()) return "New password is required";
        if (value.length < 8) return "Password must be at least 8 characters";
        if (!/[A-Z]/.test(value))
          return "Password must contain at least one uppercase letter";
        if (!/[0-9]/.test(value))
          return "Password must contain at least one number";
        if (!/[!@#$%^&*(),.?":{}|<>]/.test(value))
          return "Password must contain at least one special character";
        break;

      case "confirmPassword":
        if (value !== allValues.newPassword) return "Passwords do not match";
        break;

      default:
        break;
    }
    return "";
  };

  const runValidation = (values) => {
    const newErrors = {};
    Object.keys(values).forEach((key) => {
      const error = runFieldValidation(key, values[key], values);
      if (error) newErrors[key] = error;
    });
    return newErrors;
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setSubmitted(true);
    setSuccessMessage(""); 

    const validationErrors = runValidation(form);
    setErrors(validationErrors);

    if (Object.keys(validationErrors).length > 0) {
      return;
    }

    // Temporary Password
    if (form.currentPassword !== "123456") {
      setErrors((prev) => ({
        ...prev,
        currentPassword: "Current password is incorrect",
      }));
      return;
    }

    console.log("Form submitted successfully!", form);
    setSuccessMessage(" Your changes have been saved successfully!");
  };

  const handleCancel = () => {
    setForm({
      firstName: "",
      lastName: "",
      email: "",
      address: "",
      currentPassword: "",
      newPassword: "",
      confirmPassword: "",
    });
    setErrors({});
    setSubmitted(false);
    setSuccessMessage("");  
  };

  return (
    <div className="account-container">
      <aside className="account-sidebar">
        <h3>Manage My Account</h3>
        <ul>
          <li>
            <a className="active" href="#">
              My Profile
            </a>
          </li>
          <li>
            <a href="#">Address Book</a>
          </li>
          <li>
            <a href="#">My Payment Options</a>
          </li>
        </ul>

        <h3>My Orders</h3>
        <ul>
          <li>
            <a href="#">My Returns</a>
          </li>
          <li>
            <a href="#">My Cancellations</a>
          </li>
        </ul>

        <h3>My Wishlist</h3>
      </aside>

     
      <main className="account-content">
        <h2>Edit Your Profile</h2>

        
        {successMessage && <p className="success-message">{successMessage}</p>}

        <form className="profile-form" onSubmit={handleSubmit}>
          <div className="form-grid">
            <div>
              <label>First Name</label>
              <input
                type="text"
                name="firstName"
                value={form.firstName}
                onChange={handleChange}
                onBlur={handleBlur}
                placeholder="Md"
              />
              {errors.firstName && <p className="error">{errors.firstName}</p>}
            </div>
            <div>
              <label>Last Name</label>
              <input
                type="text"
                name="lastName"
                value={form.lastName}
                onChange={handleChange}
                onBlur={handleBlur}
                placeholder="Rimel"
              />
              {errors.lastName && <p className="error">{errors.lastName}</p>}
            </div>
            <div>
              <label>Email</label>
              <input
                type="email"
                name="email"
                value={form.email}
                onChange={handleChange}
                onBlur={handleBlur}
                placeholder="rimel111@gmail.com"
              />
              {errors.email && <p className="error">{errors.email}</p>}
            </div>
            <div>
              <label>Address</label>
              <input
                type="text"
                name="address"
                value={form.address}
                onChange={handleChange}
                onBlur={handleBlur}
                placeholder="Kingston, 5236, United State"
              />
              {errors.address && <p className="error">{errors.address}</p>}
            </div>
          </div>

          <label style={{ marginTop: 25 }}>Password Changes</label>
          <input
            type="password"
            name="currentPassword"
            value={form.currentPassword}
            onChange={handleChange}
            onBlur={handleBlur}
            placeholder="Current Password"
          />
          {errors.currentPassword && (
            <p className="error">{errors.currentPassword}</p>
          )}

          <input
            type="password"
            name="newPassword"
            value={form.newPassword}
            onChange={handleChange}
            onBlur={handleBlur}
            placeholder="New Password"
          />
          {errors.newPassword && <p className="error">{errors.newPassword}</p>}

          <input
            type="password"
            name="confirmPassword"
            value={form.confirmPassword}
            onChange={handleChange}
            onBlur={handleBlur}
            placeholder="Confirm New Password"
          />
          {errors.confirmPassword && (
            <p className="error">{errors.confirmPassword}</p>
          )}

          <div className="form-actions">
            <button type="button" className="btn-cancel" onClick={handleCancel}>
              Cancel
            </button>
            <button type="submit" className="btn-save">
              Save Changes
            </button>
          </div>
        </form>
      </main>
    </div>
  );
}

namespace MusicBookingApp.Domain.ServiceErrors;

public static partial class Errors
{
    public static class Auth
    {
        public static Error LoginFailed => Error.Failure(
            code: "Auth.LoginFailed",
            description: "Username or password are incorrect.");

        public static Error InvalidOtp => Error.Failure(code: "Auth.InvalidOtp", description: "The OTP provided is invalid.");

        public static Error OtpExpired => Error.Failure(code: "Auth.OtpExpired", description: "The OTP provided has expired.");

        public static Error EmailVerificationFailed => Error.Failure(code: "Auth.EmailVerificationFailed", description: "We could not verify your email at the moment, Please try again soon.");

        public static Error UserNotFound => Error.Failure(code: "Auth.UserNotFound", description: "User not found.");

        public static Error PasswordChangeFailed => Error.Failure(code: "Auth:PasswordChangeFailed", description: "Something went wrong while trying to change your password, please try again");

        public static Error ResetPasswordFailed => Error.Failure(code: "Auth:ResetPasswordFailed", description: "Password reset unsuccessful. Please try again later or contact support if the issue persists.");
    }
}
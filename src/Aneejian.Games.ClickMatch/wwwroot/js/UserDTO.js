export class UserDTO {
	constructor(user) {
		this.id = user.id === 0 || null ? undefined : user.id;
		this.userName = user.userName;
		this.name = user.name;
		this.password = user.password;
		this.avatar = user.avatar;
	}
}

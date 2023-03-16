import axios from "axios";
import React,{useState, useEffect} from "react";
import 'bootstrap/dist/css/bootstrap.css';



const ListUser = () =>{
    const [users, setUsers] = useState<User[]>([]);
    const [userId, setUserId] = useState('');
    const [username, setUsername] = useState('');
    const [fullname, setFullname] = useState('');
    const [email, setEmail] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState(new Date);
    const [phone, setPhone] = useState('');
    const [address, setAddress] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();

        if (!username.trim()) {
            setError('Please enter a value');
          }
          else{
            setError('');
          }
        if (!username.trim() || !fullname.trim() || !email.trim() || !phone.trim() || !address.trim()) {

          }
        else{
            axios.post<User>(`${import.meta.env.VITE_BE}/DemoUser`, { username,fullname,email,dateOfBirth,phone,address })
        .then(response => console.log(response.data))
        .catch(error => console.error(error));

        setUsername('');
        setFullname('');
        setEmail('');
        setDateOfBirth(new Date);
        setPhone('');
        setAddress('');
        location.reload()
        }
    };

    useEffect(() => {
        axios.get<User[]>(`${import.meta.env.VITE_BE}/DemoUser`)
        .then(response => {
            setUsers(response.data.data)
        })
        .catch(error => console.log(error));
    },[])

    const handlerDelete = (id) => {
        console.log(id)
        const newListUsers = users.filter((user) => user.id !== id);
        setUsers(newListUsers);

        axios.delete<User>(`${import.meta.env.VITE_BE}/DemoUser/${id}`)
      .then(response => console.log(response.data.data))
      .catch(error => console.error(error));
    }
    const handlerEdit = (id) => {
        console.log(id)
        const userUpdate = users.find((user) => user.id === id);
        console.log(userUpdate);
        setUsername(userUpdate?.username?? "");
        setFullname(userUpdate?.fullname?? "");
        setEmail(userUpdate?.email?? "");
        setDateOfBirth(userUpdate?.dateOfBirth??new Date);
        setPhone(userUpdate?.phone?? "");
        setAddress(userUpdate?.address?? "");
        setUserId(userUpdate?.id??"");
    }
    const handlerUpdate = () => {
        axios.put<User>(`${import.meta.env.VITE_BE}/DemoUser/${userId}`, { username,fullname,email,dateOfBirth,phone,address })
        .then(response => console.log(response.data.data))
        .catch(error => console.error(error));
        location.reload()
        console.log("update")
    }
    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
    
        if (month.length < 2) 
            month = '0' + month;
        if (day.length < 2) 
            day = '0' + day;
    
        return [year, month, day].join('-');
    }
    return (
        <div>
            <form onSubmit={handleSubmit} className="row g-3 needs-validation" >
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">User Name</label>
                    <div className="col-sm-10">
                    <input type="text" className="form-control" value={username} onChange={event => setUsername(event.target.value)} />
                    {error && <div style={{color:"red"}}>{error}</div>}
                    </div>
                </div>
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">Full Name</label>
                    <div className="col-sm-10">
                    <input type="text" className="form-control" value={fullname} onChange={event => setFullname(event.target.value)} />
                    </div>
                </div>
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">Email</label>
                    <div className="col-sm-10">
                    <input type="text" className="form-control" value={email} onChange={event => setEmail(event.target.value)} />
                    </div>
                </div>
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">Date Of Birth</label>
                    <div className="col-sm-10">
                    <input type="date" id="date" name="date" value={formatDate(dateOfBirth)} onChange={event => setDateOfBirth(new Date(event.target.value))} />
                    </div>
                </div>
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">Phone Number</label>
                    <div className="col-sm-10">
                    <input type="text" className="form-control" value={phone} onChange={event => setPhone(event.target.value)} />
                    </div>
                </div>
                <div className="mb-3 row input-validate">
                    <label htmlFor="staticEmail" className="col-sm-2 col-form-label">Address</label>
                    <div className="col-sm-10">
                    <textarea className="form-control" value={address} onChange={event => setAddress(event.target.value)} />
                    </div>
                </div>
                <div className="col-auto">
                <button style={{marginRight:"20px"}} type="submit">Add User</button>
                <button type="button" onClick={() => handlerUpdate()}>Update User</button>
                </div>
            </form>
            
            <table className="table">
            <thead>
                <tr>
                <th scope="col">User name</th>
                <th scope="col">Full name</th>
                <th scope="col">Email</th>
                <th scope="col">Date of birth</th>
                <th scope="col">Phone</th>
                <th scope="col">Address</th>
                <th scope="col">Action</th>
                </tr>
            </thead>
            <tbody>
                {users.map(user => (
                    <tr key={user.id}>
                        <td>{user.username}</td>
                        <td>{user.fullname}</td>
                        <td>{user.email}</td>
                        <td>{new Date(user.dateOfBirth).toLocaleString()}</td>
                        <td>{user.phone}</td>
                        <td>{user.address}</td>
                        <td style={{borderLeft:"solid 1px #9b9fa3"}}><button type="button" onClick={() => handlerDelete(user.id)}>Remove</button></td>
                        <td><button type="button" onClick={() => handlerEdit(user.id)}>Edit</button></td>
                    </tr>
                ))}
                
            </tbody>
            </table>
        </div>
    );
}
export default ListUser;